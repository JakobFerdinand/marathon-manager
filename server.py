#!/usr/bin/env python3
"""Small dependency-free local web server for the MarathonManager frontend.

Serves src/index.html and parses Anmelder2025.xlsx (fresh on every request)
using only the Python standard library. Replace the .xlsx file on disk and
reload the page to see the new data.

Run:
    python3 server.py
    # then open http://localhost:8000
"""

import json
import sys
import zipfile
import xml.etree.ElementTree as ET
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from pathlib import Path
from urllib.parse import urlparse

BASE_DIR = Path(__file__).resolve().parent
SRC_DIR = BASE_DIR / "src"
XLSX_FILE = BASE_DIR / "Anmelder2025.xlsx"
HOST = "127.0.0.1"
PORT = 8000

_NS = "{http://schemas.openxmlformats.org/spreadsheetml/2006/main}"
def _cell_text(cell, shared):
    t = cell.get("t")
    if t == "s":
        v = cell.find(_NS + "v")
        if v is None or v.text is None:
            return None
        return shared[int(v.text)]
    if t == "inlineStr":
        is_el = cell.find(_NS + "is")
        if is_el is not None:
            return "".join(t.text or "" for t in is_el.iter(_NS + "t"))
        return None
    v = cell.find(_NS + "v")
    return v.text if v is not None else None


def _parse_xlsx(path):
    with zipfile.ZipFile(path) as zf:
        names = zf.namelist()
        ss_path = next((n for n in names if n == "xl/sharedStrings.xml"), None)
        shared = []
        if ss_path:
            root = ET.fromstring(zf.read(ss_path))
            shared = [
                "".join(t.text or "" for t in si.iter(_NS + "t"))
                for si in root.iter(_NS + "si")
            ]
        sheet_path = next((n for n in names if n.startswith("xl/worksheets/") and n.endswith(".xml")), None)
        if not sheet_path:
            raise ValueError("no worksheet found")
        sheet = ET.fromstring(zf.read(sheet_path))

        runners = []
        for idx, row in enumerate(sheet.iter(_NS + "row")):
            if idx == 0:
                continue
            cells = {}
            for c in row.iter(_NS + "c"):
                ref = c.get("r") or ""
                if not ref:
                    continue
                col = "".join(ch for ch in ref if ch.isalpha())
                value = _cell_text(c, shared)
                if value is not None:
                    cells[col] = value
            nr = cells.get("A")
            if nr is None:
                continue
            gc = cells.get("G")
            runners.append(
                {
                    "nr": int(float(str(nr))),
                    "bewerb": str(cells.get("B") or "").strip(),
                    "nachname": str(cells.get("C") or "").strip(),
                    "vorname": str(cells.get("D") or "").strip(),
                    "team": str(cells.get("E") or "").strip(),
                    "firm": 1 if cells.get("F") not in (None, "") else 0,
                    "jahrgang": None if gc in (None, "") else gc,
                    "geschl": str(cells.get("H") or "").strip(),
                }
            )
        return runners


def _load_data():
    if not XLSX_FILE.is_file():
        return {"error": f"{XLSX_FILE.name} not found", "runners": []}
    try:
        runners = _parse_xlsx(XLSX_FILE)
    except (OSError, zipfile.BadZipFile, ET.ParseError, ValueError, IndexError) as exc:
        return {"error": f"could not parse {XLSX_FILE.name}: {exc}", "runners": []}
    return {"error": None, "runners": runners}


class Handler(BaseHTTPRequestHandler):
    def log_message(self, fmt, *args):
        sys.stderr.write("%s - %s\n" % (self.address_string(), fmt % args))

    def _send(self, code, body, ctype):
        data = body.encode("utf-8")
        self.send_response(code)
        self.send_header("Content-Type", ctype)
        self.send_header("Content-Length", str(len(data)))
        self.send_header("Cache-Control", "no-store")
        self.end_headers()
        self.wfile.write(data)

    def _serve_file(self, path, ctype):
        try:
            body = path.read_bytes()
        except OSError:
            self._send(404, "not found", "text/plain; charset=utf-8")
            return
        self.send_response(200)
        self.send_header("Content-Type", ctype)
        self.send_header("Content-Length", str(len(body)))
        self.send_header("Cache-Control", "no-store")
        self.end_headers()
        self.wfile.write(body)

    def do_GET(self):
        path = urlparse(self.path).path
        if path in ("/", "/index.html"):
            self._serve_file(SRC_DIR / "index.html", "text/html; charset=utf-8")
        elif path == "/data":
            self._send(200, json.dumps(_load_data(), ensure_ascii=False), "application/json; charset=utf-8")
        elif path == "/logos/sc-mining.png":
            self._serve_file(BASE_DIR / "Logos" / "SC-Mining.png", "image/png")
        elif path == "/logos/acf.png":
            self._serve_file(BASE_DIR / "Logos" / "ACF.png", "image/png")
        else:
            self._send(404, "not found", "text/plain; charset=utf-8")


def main():
    server = ThreadingHTTPServer((HOST, PORT), Handler)
    print(f"MarathonManager running at http://{HOST}:{PORT}/  (Ctrl+C to stop)")
    try:
        server.serve_forever()
    except KeyboardInterrupt:
        print("\nstopped")


if __name__ == "__main__":
    main()
