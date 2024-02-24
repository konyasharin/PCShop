import FiltersPanel from 'components/FiltersPanel/FiltersPanel.tsx';
import React from 'react';
import TComponentTypes from 'types/TComponentTypes.ts';

const AddWindow: React.FC<TComponentTypes> = props => {
  return <FiltersPanel type={props.type} />;
};

export default AddWindow;
