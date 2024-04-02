import TComponent from 'types/components/TComponent.ts';
import TCoolerFilters from 'types/components/cooler/TCoolerFilters.ts';

type TCooler<T extends File | string> = TComponent<T> & TCoolerFilters;

export default TCooler;
