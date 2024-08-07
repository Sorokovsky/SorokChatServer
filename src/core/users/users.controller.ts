import { Controller, Get, Param, ParseIntPipe } from '@nestjs/common';
import { ApiBearerAuth, ApiTags } from '@nestjs/swagger';
import { Auth } from 'src/decorators/auth.decorator';
import { UsersService } from './users.service';

@Controller('users')
@ApiBearerAuth()
@ApiTags('users')
export class UsersController {
  constructor(private readonly usersService: UsersService) { };

  @Auth()
  @Get(":id")
  public getById(@Param('id', new ParseIntPipe()) id: number) {
    return this.usersService.tryFindById(id);
  }
};
