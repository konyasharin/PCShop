import TComponent from 'types/components/TComponent.ts';
import TPowerUnitFilters from 'types/components/powerUnit/TPowerUnitFilters.ts';

type TPowerUnit<T extends File | string> = TComponent<T> & TPowerUnitFilters;

export default TPowerUnit;
