import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {Router, RouterLink} from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIcon} from '@angular/material/icon';
import {AuthService} from '../../core/services/auth.service';
import {ProfileService} from '../../core/services/profile.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterLink,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    MatIcon
  ],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  username = '';
  password = '';
  confirmPassword = '';
  hide = true;
  error = '';

  constructor(private authService: AuthService, private profileService: ProfileService, private router: Router) {
  }

  private validatePassword(password: string): string | null {
    if (password.length < 8) {
      return 'Password must be at least 8 characters long';
    }
    if (!/[a-z]/.test(password)) {
      return 'Password must contain at least one lowercase letter';
    }
    if (!/[A-Z]/.test(password)) {
      return 'Password must contain at least one uppercase letter';
    }
    if (!/[0-9]/.test(password)) {
      return 'Password must contain at least one digit';
    }
    if (!/[^a-zA-Z0-9]/.test(password)) {
      return 'Password must contain at least one special character';
    }
    return null;
  }

  register() {
    this.error = '';
    if (!this.username || !this.password || !this.confirmPassword) {
      this.error = 'All fields are required';
      return;
    }
    if (this.password !== this.confirmPassword) {
      this.error = 'Passwords do not match';
      return;
    }

    const validationError = this.validatePassword(this.password);
    if (validationError) {
      this.error = validationError;
      return;
    }

    this.authService.register(this.username, this.password).subscribe({
      next: () => {
        this.profileService.create().subscribe({
          next: () => {
            this.router.navigate(['/todos']);
          }
        });
      },
      error: (err) => {
        this.error = err?.error?.message || 'Registration failed';
      }
    });
  }
}
