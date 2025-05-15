import { Component } from '@angular/core';
import { InvoiceService } from '../../services/invoice.service';
import { Invoice } from '../../models/invoice.model';
import { NgForOf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-invoice-list',
  imports: [NgForOf, RouterModule],
  templateUrl: './invoice-list.component.html',
  styleUrl: './invoice-list.component.css'
})
export class InvoiceListComponent {
  invoices: Invoice[] = [];

  constructor(
    private invoiceService: InvoiceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.invoiceService.getInvoices().subscribe(
      (data) => {
        this.invoices = data;
      },
      (error) => {
        console.error('Error al obtener las facturas', error);
      }
    );
  }

  
  navigateToNew() {
    this.router.navigate(['/invoices/create']);
  }
}
