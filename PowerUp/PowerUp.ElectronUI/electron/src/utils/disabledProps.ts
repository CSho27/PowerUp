export interface DisabledProps {
  disabled: true | undefined;
  title: string | undefined;
}

export interface DisabledCriterion {
  isDisabled: boolean;
  tooltipIfDisabled: string;
}

export type DisabledCriteria = DisabledCriterion[];

export function toDisabledProps(tooltipIfNotDisabled: string, ...orderedDisabledCriteria: DisabledCriterion[]): DisabledProps {
  const disabledCriterion = orderedDisabledCriteria.find(c => c.isDisabled);
  if(!!disabledCriterion)
    return { disabled: true, title: disabledCriterion.tooltipIfDisabled };
  else
    return { disabled: undefined, title: tooltipIfNotDisabled }
}

