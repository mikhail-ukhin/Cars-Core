import { ErrorHandler } from "@angular/core";
import * as Raven from 'raven-js';

export class AppErrorHandler implements ErrorHandler {
    handleError(error: any): void {
        Raven.captureException(error.originalError || error);
        console.log("error");
    }
}