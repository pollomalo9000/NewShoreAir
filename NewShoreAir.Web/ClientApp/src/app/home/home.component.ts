import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CoreApiService } from '../services/core-api.service';
import { Journey } from '../Models/journey.model ';
import { ReactiveFormsModule } from '@angular/forms';
import { ExchangeRate } from '../Models/exchangeRate.model';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public form: FormGroup;
  locations: string[] = [];
  journeys: Journey[] = [];
  curencys: string[] = [];
  message = '';
  same = false;
  cop: number;
  eur: number;
  currency:number;

  constructor(private _fb: FormBuilder, private _coreApi: CoreApiService) {
    this.form = this._initGroup();
    this.cop = 1;
    this.eur = 1;
    this.currency = 1;
    this.ConsultarTasas();

  }


  currencyChange(value: any) {

    debugger;
    this.currency= value.target.value;
    this.currency = 1;
    
    if(value.target.value == 'COP')
      this.currency = this.cop;

    if(value.target.value == 'EUR')
      this.currency = this.eur;

  }

  onKeyup(event: any) {
    event.target.value = event.target.value.toUpperCase();
  }

  private _initGroup(): FormGroup {
    return this._fb.group({
      origin: ['', Validators.required,],
      destination: ['', Validators.required,],
      roundtrip: ['', Validators.required,],

    },);
  }


  comparoCampos() {
    const origin = this.form.controls['origin'].value;
    const destination = this.form.controls['destination'].value;
    const roundtrip = this.form.controls['roundtrip'].value;
    debugger;
    this.same = origin === destination;

  }



  public onSubmit(): void {



    this.Consultar();

  }



  Consultar() {
    var origin = this.form.controls['origin'].value;
    var destination = this.form.controls['destination'].value;
    var roundtrip = this.form.controls['roundtrip'].value;

    this._coreApi
      .findAllById<Journey>('flights', `${origin}/${destination}/${roundtrip}`)
      .subscribe(
        (journeys) => {

          debugger;
          var a = journeys;
          this.journeys = journeys;

        },
        (err) => {

          this.message = err.error.error;

          //this._openModalError();
        }
      );

  }


  ConsultarTasas() {
   
    this._coreApi.findById<ExchangeRate>('currency', `convert?to=COP&from=USD&amount=1`)
      .subscribe(
        (exchangeRate) => {
          this.cop =  exchangeRate.result;               
        },
        (err) => {
          this.message = err.error.error;
        }
      );
      this._coreApi.findById<ExchangeRate>('currency', `convert?to=EUR&from=USD&amount=1`)
      .subscribe(
        (exchangeRate) => {
          this.eur =  exchangeRate.result;               
        },
        (err) => {
          this.message = err.error.error;}

      );

  }



}
