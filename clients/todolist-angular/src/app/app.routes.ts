import { Routes, CanActivateFn } from '@angular/router';
import { LoginComponent } from './features/login/login.component';
import { TodosListComponent } from './features/todos-list/todos-list.component';
import { RegisterComponent } from './features/register/register.component';

const authGuard: CanActivateFn = () => {
  const token = localStorage.getItem('access_token');
  return !!token;
};

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'todos', component: TodosListComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' }
];
