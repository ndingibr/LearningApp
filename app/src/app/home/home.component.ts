import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService, AuthenticationService } from '@app/_services';
import { tradesIncrease } from '@app/_models/tradesIncrease';
import { TradeService } from '@app/_services/trade.service';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit, OnDestroy {
    currentUser: User;
    currentUserSubscription: Subscription;
    users: User[] = [];
    tradeIncreases: tradesIncrease[] = [];

    constructor(
        private authenticationService: AuthenticationService,
        private userService: UserService,
        private tradeService: TradeService
    ) {
        this.currentUserSubscription = this.authenticationService.currentUser.subscribe(user => {
            this.currentUser = user;
        });
    }

    ngOnInit() {
        this.loadAllUsers();
        this.loadTradeIncreases();
    }

    ngOnDestroy() {
        // unsubscribe to ensure no memory leaks
        this.currentUserSubscription.unsubscribe();
    }

    deleteUser(id: number) {
        this.userService.delete(id).pipe(first()).subscribe(() => {
            this.loadAllUsers()
        });
    }

    private loadAllUsers() {
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.users = users;
        });
    }

    private loadTradeIncreases() {
        this.tradeService.getTradeIncrease().pipe(first()).subscribe(tradeIncreases => {
            this.tradeIncreases = tradeIncreases;
        });
    }
}