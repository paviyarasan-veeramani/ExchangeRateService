import { DecimalPipe, DatePipe } from '@angular/common';
import { Component, effect, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConvertResult } from '../../models/rate.model';
import { RateService } from '../../services/rate.service';

@Component({
  selector: 'app-convert',
  standalone: true,
  imports: [ReactiveFormsModule, DecimalPipe, DatePipe],
  templateUrl: './convert.component.html'
})
export class ConvertComponent {
  svc = inject(RateService);
  private fb = inject(FormBuilder);

  form = this.fb.nonNullable.group({
    amount: [100, [Validators.required, Validators.min(0.01)]],
    from: ['USD', Validators.required],
    to: ['INR', Validators.required]
  });

  result = signal<ConvertResult | null>(null);
  converting = signal(false);
  failed = signal(false);
  submitted = signal(false);

  constructor() {
    // keep the dropdown values valid once currencies load
    effect(() => {
      const list = this.svc.currencies();
      if (!list.length) return;
      const { from, to } = this.form.getRawValue();
      if (!list.includes(from)) this.form.patchValue({ from: list[0] });
      if (!list.includes(to)) this.form.patchValue({ to: list[list.length - 1] });
    });
  }

  get amountInvalid(): boolean {
    const c = this.form.controls.amount;
    return this.submitted() && c.invalid;
  }

  swap(): void {
    const { from, to } = this.form.getRawValue();
    this.form.patchValue({ from: to, to: from });
  }

  submit(): void {
    this.submitted.set(true);
    this.failed.set(false);
    if (this.form.invalid) return;
    const { from, to, amount } = this.form.getRawValue();
    this.converting.set(true);
    this.svc.convert(from, to, amount).subscribe({
      next: r => { this.result.set(r); this.converting.set(false); },
      error: () => { this.failed.set(true); this.converting.set(false); }
    });
  }
}
