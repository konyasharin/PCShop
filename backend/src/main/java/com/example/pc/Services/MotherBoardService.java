package com.example.pc.Services;

import com.example.pc.IServices.IMotherBoardService;
import com.example.pc.Models.MotherBoardModel;
import com.example.pc.dto.MotherBoardDto;
import com.example.pc.repositories.MotherBoardRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class MotherBoardService implements IMotherBoardService {

    private final MotherBoardRepo motherBoardRepo;

    private static final Logger logger = LoggerFactory.getLogger(MotherBoardService.class);

    @Autowired
    public MotherBoardService(MotherBoardRepo motherBoardRepo) {
        this.motherBoardRepo = motherBoardRepo;
    }

    @Override
    public List<MotherBoardModel> getMotherBoards() {
        return motherBoardRepo.findAll();
    }

    @Override
    public MotherBoardModel getMotherBoardById(Long id) {
        return motherBoardRepo.findById(id).orElse(null);
    }

    @Override
    public MotherBoardModel updateMotherBoard(MotherBoardModel motherBoardModel) {
        return motherBoardRepo.save(motherBoardModel);
    }

    @Override
    public void deleteMotherBoardById(Long id) {
        motherBoardRepo.deleteById(id);
    }

    @Override
    public MotherBoardModel createMotherBoard(MotherBoardDto motherBoardDto){

        try {
            byte[] imageData = motherBoardDto.getImage().getBytes();
            return motherBoardRepo.save(MotherBoardModel.builder()
                    .brand(motherBoardDto.getBrand())
                    .model(motherBoardDto.getModel())
                    .description(motherBoardDto.getDescription())
                    .price(motherBoardDto.getPrice())
                    .country(motherBoardDto.getCountry())
                    .frequency(motherBoardDto.getFrequency())
                    .socket(motherBoardDto.getSocket())
                    .chipset(motherBoardDto.getSocket())
                    .image(imageData)
                    .build());
        }catch(IOException e){

            logger.error("Failed to convert MotherBoard image to byte array", e);
            throw  new RuntimeException("Failed to convert MotherBoard image to byte array", e);
        }
    }

}
