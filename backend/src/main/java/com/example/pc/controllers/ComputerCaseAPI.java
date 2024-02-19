package com.example.pc.controllers;

import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.Services.*;
import com.example.pc.dto.ComputerCaseDto;
import lombok.AllArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.multipart.MultipartFile;

@RestController
@AllArgsConstructor
public class ComputerCaseAPI {

    private static final Logger logger = LoggerFactory.getLogger(ComputerCaseAPI.class);

    @Autowired
    private ComputerCaseService computerCaseService;

    @PostMapping("/createComponent/ComputerCase")
    public ResponseEntity<ComputerCaseModel> createComputerCase(@RequestBody ComputerCaseDto computerCaseDto){

        MultipartFile image = computerCaseDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("ComputerCase created successfully: {}", computerCaseDto);

        return new ResponseEntity<>(computerCaseService.createComputerCase(computerCaseDto),
                HttpStatus.CREATED);

    }

}
