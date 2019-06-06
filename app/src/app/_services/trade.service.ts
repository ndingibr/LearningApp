import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { tradesIncrease } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class TradeService {
    constructor(private http: HttpClient) { }

    getTradeIncrease() {
        return this.http.get<tradesIncrease[]>(`${environment.apiUrl}/trade/tradesincrease`);
    }

}