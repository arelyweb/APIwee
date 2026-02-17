import { Component, OnInit,Injectable, inject, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

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
  imports: [CommonModule, RouterOutlet],
  standalone: true,
})

@Injectable({providedIn: 'root'})

export class App {
  private http = inject(HttpClient);
   public _policy: Policy[] = [];

     ngOnInit() {
    this.getPolicy();
  }

    getPolicy() {
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
