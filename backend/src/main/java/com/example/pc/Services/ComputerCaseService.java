package com.example.pc.Services;

import com.example.pc.IServices.IComputerCaseService;
import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.dto.ComputerCaseDto;
import com.example.pc.repositories.ComputerCaseRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class ComputerCaseService implements IComputerCaseService {

    private final ComputerCaseRepo computerCaseRepo;

    private static final Logger logger = LoggerFactory.getLogger(ComputerCaseService.class);

    @Autowired
    public ComputerCaseService(ComputerCaseRepo computerCaseRepo) {
        this.computerCaseRepo = computerCaseRepo;
    }

    @Override
    public List<ComputerCaseModel> getAllComputerCases() {
        return computerCaseRepo.findAll();
    }

    @Override
    public ComputerCaseModel getComputerCaseById(Long id) {
        return computerCaseRepo.findById(id).orElse(null);
    }

    @Override
    public ComputerCaseModel updateComputerCase(ComputerCaseModel computerCaseModel) {
        return computerCaseRepo.save(computerCaseModel);
    }

    @Override
    public void deleteComputerCaseById(Long id) {
        computerCaseRepo.deleteById(id);
    }

    @Override
    public ComputerCaseModel createComputerCase(ComputerCaseDto computerCaseDto){

        try {
            byte[] imageData = computerCaseDto.getImage().getBytes();
            return computerCaseRepo.save(ComputerCaseModel.builder()
                    .brand(computerCaseDto.getBrand())
                    .model(computerCaseDto.getModel())
                    .description(computerCaseDto.getDescription())
                    .price(computerCaseDto.getPrice())
                    .country(computerCaseDto.getCountry())
                    .material(computerCaseDto.getMaterial())
                    .width(computerCaseDto.getWidth())
                    .height(computerCaseDto.getHeight())
                    .depth(computerCaseDto.getDepth())
                    .image(imageData)
                    .build());
        }catch (IOException e){

            logger.error("Failed to convert ComputerCase image to byte array", e);
            throw  new RuntimeException("Failed to convert ComputerCase image to byte array", e);
        }
    }

}
