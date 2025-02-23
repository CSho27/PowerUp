export interface Dict {
  [key: string]: string | undefined;
}

export class ContentDisposition implements Dict {
  [key: string]: string | undefined;

  constructor(disposition: string | null) {
    if (!disposition) return;
    disposition.split(';').forEach(p => {
      const innerParts = p.trim().split('=');
      this[innerParts[0]!] = innerParts[1];
    });
  }
}
