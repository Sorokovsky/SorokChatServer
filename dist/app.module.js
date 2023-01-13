"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppModule = void 0;
const common_1 = require("@nestjs/common");
const mongoose_1 = require("@nestjs/mongoose");
const users_module_1 = require("./users/users.module");
const dotenv_1 = require("dotenv");
const authorization_module_1 = require("./authorization/authorization.module");
const file_module_1 = require("./file/file.module");
const serve_static_1 = require("@nestjs/serve-static");
const path = require("path");
(0, dotenv_1.config)();
const DBURL = process.env.DBURL;
let AppModule = class AppModule {
};
AppModule = __decorate([
    (0, common_1.Module)({
        imports: [
            mongoose_1.MongooseModule.forRootAsync({
                useFactory: () => ({ uri: DBURL })
            }),
            users_module_1.UsersModule,
            authorization_module_1.AuthorizationModule,
            file_module_1.FileModule,
            serve_static_1.ServeStaticModule.forRoot({ rootPath: path.join(__dirname, 'static') })
        ]
    })
], AppModule);
exports.AppModule = AppModule;
;
//# sourceMappingURL=app.module.js.map