import { HttpClient } from '@angular/common/http';
import buildQuery, { QueryOptions}  from 'odata-query';
import { Observable } from 'rxjs';

export abstract class ODataService<T> {
  protected abstract baseUrl: string;

  constructor(protected http: HttpClient) {}

  getAll(queryOptions?: Partial<QueryOptions<T>>): Observable<T[]> {
    const query = buildQuery(queryOptions);
    return this.http.get<T[]>(`${this.baseUrl}${query}`);
  }

  getById(id: string | number, queryOptions?: Partial<QueryOptions<T>>): Observable<T> {
    const query = buildQuery(queryOptions);
    return this.http.get<T>(`${this.baseUrl}(${id})${query}`);
  }
}
