export class CreateUserDto{  
  readonly email: string;
  readonly surname: string;
  readonly name: string;
  readonly nickname?: string;
}