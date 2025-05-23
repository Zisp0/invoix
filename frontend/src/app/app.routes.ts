import { Routes } from '@angular/router';
import { InvoiceListComponent } from './components/invoice-list/invoice-list.component';
import { InvoiceFormComponent } from './components/invoice-form/invoice-form.component';
import { InvoiceDetailComponent } from './components/invoice-detail/invoice-detail.component';

export const routes: Routes = [
    { path: '', redirectTo: 'invoices', pathMatch: 'full' },
    { path: 'invoices', component: InvoiceListComponent },
    { path: 'invoices/create', component: InvoiceFormComponent },
    { path: 'invoices/:id', component: InvoiceDetailComponent },
    { path: 'invoices/:id/edit', component: InvoiceFormComponent }
];
