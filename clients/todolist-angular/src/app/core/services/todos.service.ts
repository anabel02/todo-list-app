import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environment';
import {Observable} from 'rxjs';
import {ODataService} from './odata-base.service';
import {Todo} from '../models/todo.model';

export interface CreateTodo {
  task: string;
}

export interface UpdateTodo {
  task: string;
}

@Injectable({providedIn: 'root'})
export class TodoService extends ODataService<Todo> {
  protected override baseUrl = `${environment.apiBaseUrl}/toDos`;

  constructor(http: HttpClient) {
    super(http);
  }

  create(body: CreateTodo): Observable<Todo> {
    return this.http.post<Todo>(this.baseUrl, body);
  }

  update(id: number, body: UpdateTodo): Observable<Todo> {
    return this.http.put<Todo>(`${this.baseUrl}/${id}`, body);
  }

  complete(id: number): Observable<Todo> {
    return this.http.put<Todo>(`${this.baseUrl}/${id}/complete`, {});
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
