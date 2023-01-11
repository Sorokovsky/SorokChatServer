"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@nestjs/core");
const app_module_1 = require("./app.module");
const dotenv_1 = require("dotenv");
if (dotenv_1.config) {
    (0, dotenv_1.config)();
}
const PORT = Number(process.env.PORT) || 5001;
async function bootstrap() {
    const app = await core_1.NestFactory.create(app_module_1.AppModule);
    await app.listen(PORT, () => {
        console.log(`Server has been started on port: ${PORT}`);
    });
}
bootstrap();
//# sourceMappingURL=main.js.map