import { Component, inject } from '@angular/core';
import { ConvertComponent } from './components/convert/convert.component';
import { RatesComponent } from './components/rates/rates.component';
import { RateService } from './services/rate.service';

@Component({
  selector: 'app-root',
  imports: [ConvertComponent, RatesComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  svc = inject(RateService);
  ngOnInit(): void {
    this.svc.loadRates();
    this.svc.checkHealth();
  }
}
