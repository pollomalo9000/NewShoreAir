import { Transport } from "./transport.model";

export class Flight 
{


    constructor() {
       this.transport = new Transport();
      }

      transport:Transport;
      origin:string = '';
      destination:string = '';
      price:number = 0;




}