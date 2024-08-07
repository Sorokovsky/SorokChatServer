import { createParamDecorator, ExecutionContext, BadRequestException } from '@nestjs/common';
import { Request } from "express";
import { UserKey } from "src/contracts/user/key.type";
import { User } from "src/database/entities/user.entity";

export const CurrentUser = createParamDecorator(
    (key: UserKey | undefined | null, context: ExecutionContext) => {
        const request: Request = context.switchToHttp().getRequest();
        const user: User | undefined = request['user'];
        if (user === null || user === undefined) throw new BadRequestException("User not found");
        return key ? user[key] : user;
    }
);