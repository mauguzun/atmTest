import { Component } from '@angular/core';
import { Money } from 'src/Models/Money';
import { AtmService } from './atm.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})

export class AppComponent {

  card: string | null = null;
  cardSelected: string | null = null;
  balance: string | null = null;
  amount: number | null = null;

  cash: Money | null = null;

  constructor(public atmService: AtmService) {

  }

  setCard() {

    this.cardSelected = null;
    this.atmService.set(this.card).subscribe(() => {
      this.cardSelected = this.card;
    }, err => {
      alert(err.error);
    });
  }

  returnCard() {
    this.atmService.return().subscribe(() => {
      this.cardSelected = null;
    }, err => {
      alert(err.error);
    });
  }

  checkBalance() {
    this.balance = null;
    this.atmService.balance().subscribe((balance) => {
      this.balance = balance;
    }, err => {
      alert(err.error);

    });
  }

  withdraw() {
    this.atmService.withdraw( this.amount * 1 ).subscribe((cash: Money) => {
      this.cash = cash;
    }, err => {
      alert(err.error);
    });
  }

}
