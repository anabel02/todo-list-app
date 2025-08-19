import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environment';
import { Observable } from 'rxjs';

export interface Todo {
  id: number;
  task: string;
  completedDateTime?: string;
}

export interface CreateTodo {
  task: string;
}

export interface UpdateTodo {
  task: string;
}

@Injectable({ providedIn: 'root' })
export class TodoService {
  private apiUrl = `${environment.apiBaseUrl}/toDos`;

  constructor(private http: HttpClient) {}

  getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.apiUrl);
  }

  getTodoById(id: number): Observable<Todo> {
    return this.http.get<Todo>(`${this.apiUrl}/${id}`);
  }

  createTodo(body: CreateTodo): Observable<Todo> {
    return this.http.post<Todo>(this.apiUrl, body);
  }

  updateTodo(id: number, body: UpdateTodo): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, body);
  }

  completeTodo(id: number): Observable<Date> {
    return this.http.put<Date>(`${this.apiUrl}/${id}/complete`, {});
  }

  deleteTodo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
