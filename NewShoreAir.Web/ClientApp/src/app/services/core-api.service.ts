import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, Observable, of, throwError } from 'rxjs';
import { HttpHeadersEnum } from '../enums/http-headers.enum';


@Injectable({
  providedIn: 'root',
})
export class CoreApiService {
  /**
   * Cabeceras HTTP
   */
  public headers: HttpHeaders;
  /**
   * Lista de rutas que responden a cada entidad.
   */
  private routes: any;

  constructor(
    private _http: HttpClient,
     private router: Router
  ) {
    this.headers = new HttpHeaders().set(
      HttpHeadersEnum.CONTENT_TYPE,
      HttpHeadersEnum.APPLICATION_JSON,
    );
    this.headers.append( 'apikey',
    '08OInHqSoeux2HPGs0bukF4j6nQIU7AX')
    
    this._initRoutes();
  }
  /**
   * Inicializa el objeto contenedor de las rutas especificadas
   */
  private _initRoutes(): void {
    this.routes = {
      flights: {
        backendURL: `${this.apiURL}Flight/GetJourneys`,
        searchURL: `${this.apiURL}Flight/GetJourneys`,
        backendURL2: `${this.apiURL}Flight/GetJourneys`,
      },
      currency: {
        backendURL: `${this.apilayerCurrency}`,
        searchURL: `${this.apilayerCurrency}`,
        backendURL2: `${this.apilayerCurrency}`,
      }

     };


     
  }

  /**
   * Devuelve la URL correspondiente a la API
   * seteada en el rootConfig
   */
  get apiURL(): string {

    return  `https://localhost:7070/Api/` ;
  }

  get apilayerCurrency(): string {

    return  `https://api.apilayer.com/exchangerates_data` ;
  }

  /**
   * Maneja una operación HTTP fallida
   * para que la aplicación siga su rumbo
   * @returns { Observable<any> }
   */
  private _handleObsError(err: any) {
    console.error('An error occurred:', err);
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('Error:', err.error.message);
      return throwError(err.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      console.error(
        `Backend returned code ${err.status}, body was: ${JSON.stringify(
          err.error
        )}`
      );
      return throwError(err);
    }
  }
  /**
   * Construye los HttpParams según la query indicada.
   * @param query
   * @returns { HttpParams }
   */
  private _buildParams(query: any): HttpParams {
    return Object.getOwnPropertyNames(query).reduce(
      (p, key) => p.append(key, query[key]),
      new HttpParams()
    );
  }
  /**
   * Indicando una entidad y su id, devuelve el registro pertinente a la misma.
   * @param type Entidad a buscar
   * @param id Id de la entidad
   * @returns {Obsevable<T>}
   */

   public findObject<T>(type: string): Observable<T> {
    const url = `${this.routes[type].backendURL}`;
    return this._http.get<T>(url);
  }

  public findById<T>(type: string, id: string): Observable<T> {
    const url = `${this.routes[type].backendURL}/${id}`;
    debugger;
    var a = this._http.get<T>(url);
    return a;
  }
  /**
   * Indicando una entidad y su id, devuelve el registro pertinente a la misma.
   * @param type Entidad a buscar
   * @param id Id de la entidad
   * @returns {Obsevable<T>}
   */
  public findById2<T>(type: string, id: string): Observable<T> {
    const url = `${this.routes[type].backendURL2}/${id}`;
    return this._http.get<T>(url);
  }


 /**
   * Indicando la entidad a buscar devuelve todos los registros pertinentes de la misma
   * @param type Entidad a buscar
   * @returns {Obsevable<T[]>}
   */
  findAllById<T>(type: string, id: string): Observable<T[]> {
    const url = `${this.routes[type].backendURL}/${id}`;

    return this._http
      .get<T[]>(url)
      .pipe(catchError((err) => this._handleObsError(err)));
  }


  /**
   * Indicando la entidad a buscar devuelve todos los registros pertinentes de la misma
   * @param type Entidad a buscar
   * @returns {Obsevable<T[]>}
   */
  findAll<T>(type: string): Observable<T[]> {
    const url = `${this.routes[type].backendURL}`;

    return this._http
      .get<T[]>(url)
      .pipe(catchError((err) => this._handleObsError(err)));
  }
  /**
   * Indicando una entidad y los criterios de búsqueda, devuelve aquellos registros
   * que cumplan con ese determinado criterio
   * @param type Entidad a buscar
   * @param query Criterios de búsqueda
   * @returns {Obsevable<T[]>}
   */
  public findByQuery<T>(type: string, query: any): Observable<T[]> {
    const searchUrl = `${this.routes[type].searchURL}`;
    const params = this._buildParams(query);
    return this._http
      .get<T[]>(searchUrl, { params })
      .pipe(catchError((err) => this._handleObsError(err)));
  }

  /**
   * Indicando una entidad y los criterios de búsqueda, devuelve aquellos registros
   * que cumplan con ese determinado criterio
   * @param type Entidad a buscar
   * @param query Criterios de búsqueda
   * @returns {Obsevable<T>}
   */
 

 /**
   * Indicando una entidad y los criterios de búsqueda, devuelve aquellos registros
   * que cumplan con ese determinado criterio
   * @param type Entidad a buscar
   * @param query Criterios de búsqueda
   * @returns {Obsevable<T[]>}
   */
  public findByObject<T>(type: string, entity: any): Observable<T[]> {
   
       const url = `${this.routes[type].backendURL}`;

    return this._http.post<T[]>(url, JSON.stringify(entity), {
      headers: this.headers,
    });
  }

  /**
   * Indicando una entidad y un registro que persiga ese modelo de datos, lo da de alta.
   * @param type Entidad
   * @param entity Registro a dar de alta
   * @returns {Obsevable<T>}
   */
  public create<T>(type: string, entity: any): Observable<T> {
    const url = `${this.routes[type].createURL}`;
    return this._http.post<T>(url, JSON.stringify(entity), {
      headers: this.headers,
    });
  }

  /**
   * Indicando una entidad, el id del registro perteneciente a la misma
   * y el registro actualizado, actualiza el registro existente.
   * @param type Entidad
   * @param id Id del registro perteneciente a esa entidad
   * @param entity Registro editado
   * @returns {Obsevable<T>}
   */
  public update<T>(type: string, id: string, entity: any): Observable<T> {
    const url = `${this.routes[type].backendURL}/${id}`;
    return this._http.put<T>(url, JSON.stringify(entity), {
      headers: this.headers,
    });
  }
  /**
   * Indicando una entidad y el id del registro pertenenciente a la misma
   * elimina dicho registro.
   * @param type Entidad
   * @param id Id del registro a eliminar
   * @returns {Observable<T>}
   */
  public delete<T>(type: string, id: string): Observable<T> {
    const url = `${this.routes[type].backendURL}/${id}`;
    return this._http.delete<T>(url, { headers: this.headers });
  }
}
