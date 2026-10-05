export interface ExchangeRate {
  id: number;
  fromCurrency: string;
  toCurrency: string;
  rate: number;
  updatedAt: string;
}

/** Match this to what GET /api/convert returns from your controller. */
export interface ConvertResult {
  from: string;
  to: string;
  amount: number;
  rate: number;
  result: number;
}
