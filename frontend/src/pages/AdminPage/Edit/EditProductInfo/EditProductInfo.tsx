import React, { ReactNode } from 'react';
import { TFiltersType } from 'hooks/useFilters.ts';
import EComponentTypes from 'enums/EComponentTypes.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import EditFilterBlock from '../EditFilterBlock/EditFilterBlock.tsx';
import Radio from 'components/Radio/Radio.tsx';
import styles from './EditProductInfo.module.css';
import Input from 'components/Input/Input.tsx';

type EditProductInfoProps = {
  type: EComponentTypes;
  price: number;
  setPrice: (newPrice: number) => void;
  productName: string;
  setProductName: (newProductName: string) => void;
  img: string;
  imgFileRef: React.MutableRefObject<null | File>;
  setImg: React.Dispatch<React.SetStateAction<string>>;
  setDescription: (newValue: string) => void;
  description: string;
  setAmount: (newValue: number) => void;
  amount: number;
  filters: TFiltersType;
  setCheckBoxIsActive: (
    nameBlock: string,
    index: number,
    newIsActive: boolean,
  ) => void;
  setRadioIsActive: (nameBlock: string, index: number) => void;
};

const EditProductInfo: React.FC<EditProductInfoProps> = props => {
  const filtersBlocks: ReactNode[] = [];
  props.filters.forEach(filter => {
    if (filter.type === 'checkBox') {
      const filterElements = filter.filters.map((filterElem, i) => {
        return (
          <CheckBox
            text={filterElem.text}
            isActive={filterElem.isActive}
            onChange={() => {
              props.setCheckBoxIsActive(filter.name, i, !filterElem.isActive);
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
          title={filter.text}
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
              props.setRadioIsActive(filter.name, i);
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
          title={filter.text}
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
        onChange={e => {
          if (e.target.files) {
            props.setImg(URL.createObjectURL(e.target.files[0]));
            props.imgFileRef.current = e.target.files[0];
          }
        }}
        accept={'image/*'}
      />
      <label htmlFor="editProductImg">
        <img src={props.img} alt="componentImg" className={styles.img} />
      </label>
      <EditFilterBlock
        title={'Цена'}
        filterBlock={
          <Input
            value={`${props.price}`}
            placeholder={'Цена товара'}
            onChange={newValue => props.setPrice(+newValue)}
            className={styles.priceInput}
            type={'number'}
          />
        }
        className={styles.filterBlock}
      />
      <EditFilterBlock
        title={'Описание'}
        filterBlock={
          <Input
            value={props.description}
            placeholder={'Описание товара'}
            onChange={newValue => props.setDescription(newValue)}
            className={styles.priceInput}
          />
        }
        className={styles.filterBlock}
      />
      <EditFilterBlock
        title={'Количество'}
        filterBlock={
          <Input
            value={`${props.amount}`}
            placeholder={'Количество товара'}
            onChange={newValue => props.setAmount(+newValue)}
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
