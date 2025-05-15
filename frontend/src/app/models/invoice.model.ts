import { InvoiceDetail } from "./invoice-detail.model";

export interface Invoice {
  id: number;
  client: string;
  date: string;
  total: number;
  details: InvoiceDetail[];
}
