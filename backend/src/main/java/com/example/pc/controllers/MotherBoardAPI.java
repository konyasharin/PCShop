package com.example.pc.controllers;

import com.example.pc.Models.MotherBoardModel;
import com.example.pc.Services.MotherBoardService;
import com.example.pc.dto.MotherBoardDto;
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
public class MotherBoardAPI {

    private static final Logger logger = LoggerFactory.getLogger(MotherBoardAPI.class);

    @Autowired
    private MotherBoardService motherBoardService;

    @PostMapping
    public ResponseEntity<MotherBoardModel> createMotherBoard(@RequestBody MotherBoardDto motherBoardDto){

        MultipartFile image = motherBoardDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("MotherBoard created successfully: {}", motherBoardDto);

        return new ResponseEntity<>(motherBoardService.createMotherBoard(motherBoardDto),
                HttpStatus.OK);
    }
}
