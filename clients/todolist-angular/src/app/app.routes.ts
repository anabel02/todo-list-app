import { Routes, CanActivateFn } from '@angular/router';
import { LoginComponent } from './features/login/login.component';
import { TodosListComponent } from './features/todos-list/todos-list.component';

const authGuard: CanActivateFn = () => {
  const token = localStorage.getItem('access_token');
  return !!token;
};

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  { path: 'todos', component: TodosListComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' }
];
