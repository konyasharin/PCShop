package com.example.pc.controllers;

import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.Models.RAMModel;
import com.example.pc.Services.RAMService;
import com.example.pc.dto.RamDto;
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
public class RAMAPI {

    private static final Logger logger = LoggerFactory.getLogger(RAMAPI.class);

    @Autowired
    private RAMService ramService;

    @PostMapping("/createComponent/RAM")
    public ResponseEntity<RAMModel> createRAM(@RequestBody RamDto ramDto){

        MultipartFile image = ramDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("RAM created successfully: {}", ramDto);

        return new ResponseEntity<>(ramService.createRAM(ramDto),
                HttpStatus.CREATED);



    }

}
