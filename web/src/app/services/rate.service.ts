import { HttpClient } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ConvertResult, ExchangeRate } from '../models/rate.model';

@Injectable({ providedIn: 'root' })
export class RateService {
 
  private http = inject(HttpClient);
  private api = environment.apiUrl;

  rates = signal<ExchangeRate[]>([]);
  loading = signal(true);
  error = signal(false);
  apiUp = signal(true);

  currencies = computed(() => {
    const set = new Set<string>();
    this.rates().forEach(r => { set.add(r.fromCurrency); set.add(r.toCurrency); });
    return [...set].sort();
  });

  lastUpdated = computed(() => {
    const times = this.rates().map(r => new Date(r.updatedAt).getTime());
    return times.length ? new Date(Math.max(...times)) : null;
  });

  loadRates(): void {
    this.loading.set(true);
    this.error.set(false);
    this.http.get<ExchangeRate[]>(`${this.api}/api/rates`).subscribe({
      next: data => { this.rates.set(data); this.loading.set(false); },
      error: () => { this.error.set(true); this.loading.set(false); }
    });
  }

  checkHealth(): void {
    this.http.get(`${this.api}/health`, { responseType: 'text' }).subscribe({
      next: () => this.apiUp.set(true),
      error: () => this.apiUp.set(false)
    });
  }

  convert(from: string, to: string, amount: number): Observable<ConvertResult> {
    return this.http.get<ConvertResult>(`${this.api}/api/convert`, { params: { from, to, amount } });
  }
}
