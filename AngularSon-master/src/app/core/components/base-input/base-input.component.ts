import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ControlContainer, FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { BaseInputErrorsComponent } from '../base-input-errors/base-input-errors.component';

@Component({
  selector: 'myinput',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, BaseInputErrorsComponent],
  templateUrl: './base-input.component.html',
  styleUrl: './base-input.component.scss'
})
export class BaseInputComponent {
  @Input() label!: string;
  @Input() labelId!: string;
  @Input() inputType: string = 'text';
  @Input() controlName!: string;
  @Input() placeholder!: string;

  constructor(private controlContainer: ControlContainer) {}

  get form() {
    if (this.controlContainer?.formDirective) {
      return (this.controlContainer.formDirective as FormGroupDirective).form;
    }
    throw new Error(
      'This component should be used inside a reactive form group.'
    );
}

}