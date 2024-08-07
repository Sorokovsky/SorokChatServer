import { UseGuards } from "@nestjs/common";
import { AuthGuard } from '../guards/auth/auth.guard';

export const Auth = () => UseGuards(AuthGuard);