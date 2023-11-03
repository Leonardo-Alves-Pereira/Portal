// auth.guard.ts

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { LoginService } from '../service/login.service';

@Injectable({
    providedIn: 'root'
})

export class NoAuthGuard implements CanActivate {
    constructor(private authService: LoginService, private router: Router) { }

    canActivate(): boolean {
        if (!this.authService.isLogado()) {
            return true;
        }

        this.router.navigate([`/tarefas/lista`]);
        return false;
    }
}