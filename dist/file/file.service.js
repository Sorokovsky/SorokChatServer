"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.FileService = void 0;
const common_1 = require("@nestjs/common");
const fs = require("fs");
const path = require("path");
let FileService = class FileService {
    async uploadAvatar(avatar, id) {
        try {
            const avatarPath = path.join(`${id}`, `avatar.${avatar.originalname.split('.').pop()}`);
            const avatarUrl = path.resolve(__dirname, '..', 'static', avatarPath);
            const avatarDirUrl = path.resolve(__dirname, '..', 'static', `${id}`);
            if (!fs.existsSync(avatarDirUrl))
                fs.mkdirSync(avatarDirUrl, { recursive: true });
            const prevAvatar = await this.checkAvatar(avatarDirUrl);
            if (prevAvatar)
                fs.rmSync(path.join(avatarDirUrl, prevAvatar));
            fs.writeFileSync(avatarUrl, avatar.buffer);
            return avatarPath;
        }
        catch (error) {
            throw new common_1.HttpException(error.message, common_1.HttpStatus.BAD_REQUEST);
        }
    }
    async checkAvatar(dir) {
        const items = fs.readdirSync(dir);
        for (let i = 0; i < items.length; i++) {
            const el = items[i];
            if (el.startsWith('avatar')) {
                return el;
            }
        }
        return null;
    }
    async deleteAvatar(id) {
        try {
            const dirPath = path.resolve(__dirname, '..', 'static', `${id}`);
            const avatar = await this.checkAvatar(dirPath);
            if (!avatar)
                throw new common_1.HttpException('Avatar not founded', common_1.HttpStatus.BAD_REQUEST);
            const avatarPath = path.join(dirPath, avatar);
            fs.rmSync(avatarPath);
        }
        catch (error) {
            throw new common_1.HttpException(error.message, common_1.HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
};
FileService = __decorate([
    (0, common_1.Injectable)()
], FileService);
exports.FileService = FileService;
//# sourceMappingURL=file.service.js.map