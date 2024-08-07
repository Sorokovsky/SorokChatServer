import { CanActivate, ExecutionContext, Injectable } from '@nestjs/common';
import { AuthorizationService } from '../../core/authorization/authorization.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    private readonly authService: AuthorizationService
  ) { };

  async canActivate(
    context: ExecutionContext,
  ): Promise<boolean> {
    return await this.authService.isAuthenticated();
  }
}
