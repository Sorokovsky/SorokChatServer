import { Module } from '@nestjs/common';
import { CookieStorageService } from './cookie-storage.service';

@Module({
  providers: [CookieStorageService],
  exports: [CookieStorageService]
})
export class CookieStorageModule { }; 
