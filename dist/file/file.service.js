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
const pathNode = require("path");
let FileService = class FileService {
    async upload(file, path, fileName, extensions) {
        const filePath = pathNode.resolve(path, fileName);
        if (this.checkFileExists(filePath))
            throw new Error('File setted');
        return filePath;
    }
    async delele() {
    }
    async getFile() {
    }
    async checkFileExists(path) {
        return new Promise((resolve, reject) => {
            fs.access(path, fs.constants.F_OK, err => {
                resolve(!err);
            });
        });
    }
};
FileService = __decorate([
    (0, common_1.Injectable)()
], FileService);
exports.FileService = FileService;
//# sourceMappingURL=file.service.js.map