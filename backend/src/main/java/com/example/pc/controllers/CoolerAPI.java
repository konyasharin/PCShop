package com.example.pc.controllers;

import com.example.pc.Models.CoolerModel;
import com.example.pc.Services.CoolerService;
import com.example.pc.dto.CoolerDto;
import lombok.AllArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@RestController
@AllArgsConstructor
public class CoolerAPI {

    private static final Logger logger = LoggerFactory.getLogger(CoolerAPI.class);

    @Autowired
    private CoolerService coolerService;

    @PostMapping("/createComponent/Cooler")
    public ResponseEntity<CoolerModel> createCooler(@RequestBody CoolerDto coolerDto){

        MultipartFile image = coolerDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("Cooler created successfully: {}", coolerDto);

        return new ResponseEntity<>(coolerService.createCooler(coolerDto),
                HttpStatus.CREATED);

    }


}
