import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Invoice } from '../models/invoice.model';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private invoices: Invoice[] = [
    { id: 1, client: 'Carlos Pérez', date: '2025-05-10', total: 120000 },
    { id: 2, client: 'Ana Gómez', date: '2025-05-12', total: 95000 },
    { id: 3, client: 'Luis Ramírez', date: '2025-05-13', total: 220000 }
  ];

  constructor() { }

  getInvoices(): Observable<Invoice[]> {
    return of(this.invoices);
  }
}
