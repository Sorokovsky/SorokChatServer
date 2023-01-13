export class CreateUserDto{  
  readonly email: string;
  readonly surname: string;
  readonly name: string;
  readonly nickname?: string;
  readonly password: string;
  readonly avatar?:string;
  readonly role?:string;
}