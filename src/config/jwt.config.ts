import { ConfigService } from "@nestjs/config";
import { JwtModuleOptions } from "@nestjs/jwt";

export const getJwtConfiguration = (configService: ConfigService): JwtModuleOptions => {
    return {
        secret: configService.get<string>("JWT_SECRET_KEY"),
    };
};