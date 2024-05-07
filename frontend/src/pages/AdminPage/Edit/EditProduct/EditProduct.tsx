import useRadios from 'hooks/useRadios.ts';
import styles from './EditProduct.module.css';
import componentTypes from 'enums/componentTypes.ts';
import React, { useContext, useEffect, useRef, useState } from 'react';
import emptyImg from 'assets/empty-img.png';
import EditProductInfo from '../EditProductInfo/EditProductInfo.tsx';
import Btn from 'components/btns/Btn/Btn.tsx';
import CategoriesBlocks from '../CategoriesBlocks/CategoriesBlocks.tsx';
import useFilters from 'hooks/useFilters.ts';
import useBorderValues from 'hooks/useBorderValues.ts';
import SearchInput from 'components/inputs/SearchInput/SearchInput.tsx';
import getComponent from 'api/components/getComponent.ts';
import initCategories from '../initCategories.ts';
import { FiltersContext } from '../../../../App.tsx';
import config from '../../../../../config.ts';
import editComponent from 'api/components/editComponent.ts';
import getImage from 'api/getImage.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import deleteComponent from 'api/components/deleteComponent.ts';

function EditProduct() {
  const allFilters = useContext(FiltersContext);
  const { radios: categories, setRadioIsActive: setCategoryIsActive } =
    useRadios(initCategories());
  const [productId, setProductId] = useState('');
  const [price, setPrice] = useBorderValues(1, 1);
  const imgFile: React.MutableRefObject<null | File> = useRef(null);
  const [description, setDescription] = useState('');
  const [amount, setAmount] = useBorderValues(0, 0);
  const [power, setPower] = useBorderValues(0, 0, 10);
  const [activeCategory, setActiveCategory] =
    useState<keyof typeof componentTypes>('videoCard');
  const [img, setImg] = useState(emptyImg);
  const imgIsUpdated = useRef(false);
  const {
    filters,
    setRadioIsActive,
    setComponentType,
    findIndexOfFilter,
    searchActivesFilters,
  } = useFilters('radio', activeCategory, allFilters);
  const [productIsGot, setProductIsGot] = useState(false);
  const dispatch = useDispatch();
  useEffect(() => {
    setComponentType(activeCategory);
  }, [activeCategory]);
  return (
    <div className={styles.block}>
      <h5 className={styles.title}>Категория</h5>
      <CategoriesBlocks
        categories={categories}
        setCategoryIsActive={setCategoryIsActive}
        setActiveCategory={setActiveCategory}
        className={styles.checkBoxes}
      />
      <SearchInput
        value={productId}
        placeholder={'Введите id товара'}
        onChange={newValue => setProductId(newValue)}
        className={styles.input}
        onSearch={() => {
          getComponent(activeCategory, +productId).then(response => {
            setPrice(response.data.price);
            setImg(`${config.backupUrl}/${response.data.image}`);
            setDescription(response.data.description);
            setAmount(response.data.amount);
            setPower(response.data.power);
            getImage(response.data.image).then(
              image =>
                (imgFile.current = new File(
                  [image.data.file],
                  response.data.image,
                  {
                    type: image.data.file.type,
                  },
                )),
            );
            if (filters) {
              let key: keyof typeof filters;
              for (key in filters) {
                const index = findIndexOfFilter(key, response.data[key]);
                if (index) {
                  setRadioIsActive(key, index);
                }
              }
            }
            setProductIsGot(true);
          });
        }}
      />
      <div style={{ display: productIsGot ? 'block' : 'none' }}>
        <EditProductInfo
          type={activeCategory}
          price={price}
          setPrice={setPrice}
          img={img}
          setImg={setImg}
          imgFileRef={imgFile}
          setDescription={setDescription}
          description={description}
          amount={amount}
          setAmount={setAmount}
          power={power}
          setPower={setPower}
          filters={filters}
          setRadioIsActive={setRadioIsActive}
          imgIsUpdated={imgIsUpdated}
        />
        <Btn
          onClick={() => {
            if (filters && imgFile.current) {
              const activeFilters = searchActivesFilters(filters);
              dispatch(setIsLoading(true));
              editComponent(
                activeCategory,
                productId,
                {
                  country: 'Russia',
                  price: price,
                  description: description,
                  image: imgFile.current,
                  amount: amount,
                  power: power,
                  ...activeFilters,
                },
                imgIsUpdated.current,
              ).then(response => {
                console.log(response.data);
                dispatch(setIsLoading(false));
              });
            }
          }}
        >
          Редактировать
        </Btn>
        <h5
          className={styles.delete}
          onClick={() => {
            dispatch(setIsLoading(true));
            deleteComponent(activeCategory, +productId).then(() =>
              dispatch(setIsLoading(false)),
            );
          }}
        >
          Удалить товар
        </h5>
      </div>
    </div>
  );
}

export default EditProduct;
