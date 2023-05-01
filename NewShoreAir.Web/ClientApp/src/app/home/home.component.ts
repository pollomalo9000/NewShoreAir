import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CoreApiService } from '../services/core-api.service';
import { Journey } from '../Models/journey.model ';
import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public form: FormGroup;
  locations:string[]=[];
  journeys:Journey[]=[];
  curencys:[]=[];
  message = '';
  same = false;
  constructor(private _fb: FormBuilder, private _coreApi: CoreApiService) {
    this.form = this._initGroup();
    this.locations= ["MZL","MDE", "PEI", "BOG","CAN","CAN", "MAD", "BCN" ,"MAD" ,"CTG" ,"MEX" ];
  

   
  }


  onKeyup(event: any) {
    event.target.value = event.target.value.toUpperCase();
  }

  private _initGroup(): FormGroup {
    return this._fb.group({
      origin: ['',Validators.required,],
      destination: ['',Validators.required,],
      roundtrip: ['',Validators.required,],

     }, );
  }


  comparoCampos() {
  const origin = this.form.controls['origin'].value;
  const destination = this.form.controls['destination'].value;
  const roundtrip = this.form.controls['roundtrip'].value;
  debugger;
  this.same = origin === destination ; 

}

  

  public onSubmit(): void {



    this.Consultar();
    
  }



  Consultar() {
  

    var origin= this.form.controls['origin'].value;
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
   
          this.message = err;

          //this._openModalError();
        }
      );
  
}



}
