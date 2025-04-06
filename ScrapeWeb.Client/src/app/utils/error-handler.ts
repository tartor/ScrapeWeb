import { WritableSignal } from '@angular/core';
import { NgForm } from '@angular/forms';

export function handleError(form: NgForm | null, error: any, errorsignal: WritableSignal<string | null>) {
  if ('error' in error) {
    let e = error.error;
    if ('error' in e) {
      errorsignal.set(e.error);
    } else if ('message' in e) {
      errorsignal.set(e.message);
    } else if ('errors' in e) {
      if (form) {
        for (const [key, value] of Object.entries(e.errors)) {
          const control = form.controls[key.toLowerCase()];
          if (control) {
            control.setErrors({ server: value });
          }
        }
      }
    } else {
      errorsignal.set("An unexpected error occurred.");
    }
  } else {
    errorsignal.set("An unexpected error occurred.");
  }
};
