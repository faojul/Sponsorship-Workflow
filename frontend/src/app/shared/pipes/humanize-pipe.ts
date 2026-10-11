import { Pipe, PipeTransform } from '@angular/core';

/**
 * Display-only helper: turns API values such as "PendingManagerApproval"
 * or "FinanceAdmin" into "Pending Manager Approval" / "Finance Admin".
 * It never changes the underlying value, only how it is shown.
 */
@Pipe({
  name: 'humanize',
})
export class HumanizePipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) {
      return '';
    }
    return value.replace(/([a-z])([A-Z])/g, '$1 $2');
  }
}
