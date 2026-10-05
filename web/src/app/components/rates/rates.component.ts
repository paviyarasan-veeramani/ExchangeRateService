import { DatePipe, DecimalPipe } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { RateService } from '../../services/rate.service';

@Component({
  selector: 'app-rates',
  standalone: true,
  imports: [DatePipe, DecimalPipe],
  templateUrl: './rates.component.html'
})
export class RatesComponent {
  svc = inject(RateService);
  search = signal('');

  filtered = computed(() => {
    const q = this.search().trim().toUpperCase();
    return this.svc.rates().filter(r =>
      !q || r.fromCurrency.toUpperCase().includes(q) || r.toCurrency.toUpperCase().includes(q));
  });

  onSearch(e: Event): void {
    this.search.set((e.target as HTMLInputElement).value);
  }

  reload(): void { this.svc.loadRates(); }
}
