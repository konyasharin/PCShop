import React, { ReactNode, useEffect } from 'react';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import EditFilterBlock from '../EditFilterBlock/EditFilterBlock.tsx';
import Radio from 'components/Radio/Radio.tsx';
import styles from './EditProductInfo.module.css';
import Input from 'components/Input/Input.tsx';

type EditProductInfoProps = {
  type: EComponentTypes;
  price: string;
  setPrice: (newPrice: string) => void;
  productName: string;
  setProductName: (newProductName: string) => void;
  img: string;
  onChangeImg: (e: React.ChangeEvent<HTMLInputElement>) => void;
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

  return (
    <>
      <Input
        value={props.productName}
        placeholder={'Введите название товара'}
        onChange={newValue => props.setProductName(newValue)}
        className={styles.input}
      />
      <input
        type="file"
        id={'editProductImg'}
        className={styles.inputImg}
        onChange={props.onChangeImg}
        accept={'image/*'}
      />
      <label htmlFor="editProductImg">
        <img src={props.img} alt="componentImg" className={styles.img} />
      </label>
      <EditFilterBlock
        title={'Цена'}
        filterBlock={
          <Input
            value={props.price}
            placeholder={'Цена товара'}
            onChange={newValue => props.setPrice(newValue)}
            className={styles.priceInput}
            type={'number'}
          />
        }
        className={styles.filterBlock}
      />
      {...filtersBlocks}
    </>
  );
};

export default EditProductInfo;
