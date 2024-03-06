import React, { ReactNode, useEffect } from 'react';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import EditFilterBlock from '../EditFilterBlock/EditFilterBlock.tsx';
import Radio from 'components/Radio/Radio.tsx';
import styles from './EditProductInfo.module.css';

type EditProductInfoProps = {
  type: EComponentTypes;
};

const EditProductInfo: React.FC<EditProductInfoProps> = props => {
  const allFiltersState = useSelector((state: RootState) => state.filters);
  const { filters, setRadioIsActive, setCheckBoxIsActive, setFiltersHandle } =
    useFilters([]);
  useEffect(() => {
    setFiltersHandle(allFiltersState[props.type]);
  }, [props.type]);
  const filtersBlocks: ReactNode[] = [];
  filters.forEach(filter => {
    if (filter.type === 'checkBox') {
      const filterElements = filter.filters.map((filterElem, i) => {
        return (
          <CheckBox
            text={filterElem.text}
            isActive={filterElem.isActive}
            onChange={() => {
              setCheckBoxIsActive(filter.name, i, !filterElem.isActive);
            }}
            className={styles.filterElement}
          />
        );
      });
      filtersBlocks.push(
        <EditFilterBlock
          filterBlock={
            <div className={styles.filterElements}>{...filterElements}</div>
          }
          title={filter.name}
          className={styles.filterBlock}
        />,
      );
    }
    if (filter.type === 'radio') {
      const filterElements = filter.filters.map((filterElem, i) => {
        return (
          <Radio
            text={filterElem.text}
            isActive={filterElem.isActive}
            onChange={() => {
              setRadioIsActive(filter.name, i);
            }}
            className={styles.filterElement}
          />
        );
      });
      filtersBlocks.push(
        <EditFilterBlock
          filterBlock={
            <div className={styles.filterElements}>{...filterElements}</div>
          }
          title={filter.name}
          className={styles.filterBlock}
        />,
      );
    }
  });

  return <>{...filtersBlocks}</>;
};

export default EditProductInfo;
