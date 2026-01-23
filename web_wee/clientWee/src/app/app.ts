import { Component, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
interface Policy {
  Id_policy: number;
  Id_client: number;
  Id_typePolicy: number;
  Id_statusPolicy: number;
  numPolicy: string;
  startDatePolicy: Date;
  endDatePolicy: Date;
  amountPolicy: Date;
}
@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: false,
})
export class App implements OnInit{
   public _policy: Policy[] = [];
   constructor(private http: HttpClient) {}

     ngOnInit() {
    this.getForecasts();
  }

    getForecasts() {
    this.http.get<Policy[]>('/api/policy').subscribe(
      (result) => {
        this._policy = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'clientWee';

}
